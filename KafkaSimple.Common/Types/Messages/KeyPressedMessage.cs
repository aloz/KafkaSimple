using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaSimple.Common.Types.Messages
{
    public class KeyPressedMessage
    {
        private readonly char _keyPressed;
        public KeyPressedMessage(char keyPressed)
        {
            this._keyPressed = keyPressed;
        }
        public char KeyPressed
        {
            get { return _keyPressed; }
        }
    }
}
