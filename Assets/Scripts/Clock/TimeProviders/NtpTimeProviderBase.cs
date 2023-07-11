using System;
using Cysharp.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace DDX.Clock.TimeProviders
{
    public abstract class NtpTimeProviderBase : ITimeProvider
    {
        protected abstract string NtpServer { get; }

        public async UniTask<TimeSpan> GetTimeAsync()
        {
            var dt = await UniTask.RunOnThreadPool(
                () => GetNetworkTimeFromNTP(NtpServer));

            return new TimeSpan(dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// Getted from https://programmera.ru/unity-lessons/kak-uznat-realnoe-vremya-v-unity-5-pri-pomoshhi-ntp-servera/
        /// </summary>
        /// <param name="ntpServer"></param>
        /// <returns></returns>
        private DateTime GetNetworkTimeFromNTP(string ntpServer)
        {
            var ntpData = new byte[48];

            //выставляем Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (без предупреждений), VN = 3 (только IPv4), Mode = 3 (Клиент)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            var ipEndPoint = new IPEndPoint(addresses[0], 123);

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                //Открываем подключение    
                socket.Connect(ipEndPoint);

                socket.ReceiveTimeout = 3000;

                //отсылваем запрос
                socket.Send(ntpData);

                //Получаем ответ
                socket.Receive(ntpData);
                socket.Close();
            }

            //Смещение для перехода в отсек с временем
            const byte serverReplyTime = 40;

            //получаем первую часть запроса и переводим биты в числа
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Получаем следующую часть и переводим биты в числа
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //меняем порядок байтов big-endian в little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            //получаем кол-во миллисекунд, прошедшее с 1900 года
            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //переводим время в **UTC**
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) + ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }
}
