using System.Globalization;
using System.Linq;
using SharpBrick.PoweredUp.Protocol.Messages;
using Xunit;

namespace SharpBrick.PoweredUp.Protocol.Formatter
{
    public class HubAlertEncoderTest
    {
        [Theory]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.EnableUpdates, 0x00, "05-00-03-01-01")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.DisableUpdates, 0x00, "05-00-03-01-02")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.RequestUpdates, 0x00, "05-00-03-01-03")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.Update, 0x00, "06-00-03-01-04-00")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.Update, 0xFF, "06-00-03-01-04-FF")]
        [InlineData(HubAlert.HighCurrent, HubAlertOperation.Update, 0x00, "06-00-03-02-04-00")]
        [InlineData(HubAlert.LowSignalStrength, HubAlertOperation.Update, 0x00, "06-00-03-03-04-00")]
        [InlineData(HubAlert.OverPowerCondition, HubAlertOperation.Update, 0x00, "06-00-03-04-04-00")]
        public void HubAlertEncoder_Encode(HubAlert alert, HubAlertOperation operation, byte payload, string expectedData)
        {
            // arrange
            var message = new HubAlertMessage() { Alert = alert, Operation = operation, DownstreamPayload = payload };

            // act
            var data = MessageEncoder.Encode(message);

            // assert
            Assert.Equal(expectedData, DataToString(data));
        }

        [Theory]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.EnableUpdates, 0x00, "05-00-03-01-01")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.DisableUpdates, 0x00, "05-00-03-01-02")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.RequestUpdates, 0x00, "05-00-03-01-03")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.Update, 0x00, "06-00-03-01-04-00")]
        [InlineData(HubAlert.LowVoltage, HubAlertOperation.Update, 0xFF, "06-00-03-01-04-FF")]
        [InlineData(HubAlert.HighCurrent, HubAlertOperation.Update, 0x00, "06-00-03-02-04-00")]
        [InlineData(HubAlert.LowSignalStrength, HubAlertOperation.Update, 0x00, "06-00-03-03-04-00")]
        [InlineData(HubAlert.OverPowerCondition, HubAlertOperation.Update, 0x00, "06-00-03-04-04-00")]
        public void HubAlertEncoder_Decode(HubAlert expectedAlert, HubAlertOperation expectedOperation, byte expectedPayload, string dataAsString)
        {
            // arrange
            var data = StringToData(dataAsString);

            // act
            var message = MessageEncoder.Decode(data) as HubAlertMessage;

            // assert
            Assert.Equal(expectedAlert, message.Alert);
            Assert.Equal(expectedOperation, message.Operation);
            Assert.Equal(expectedPayload, message.DownstreamPayload);
        }

        public static string DataToString(byte[] data)
            => string.Join("-", data.Select(b => $"{b:X2}"));
        private byte[] StringToData(string messageAsString)
            => messageAsString.Split("-").Select(s => byte.Parse(s, NumberStyles.HexNumber)).ToArray();
    }
}