////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RTIMULibCS
//
//  Copyright (c) 2015, richards-tech, LLC
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
//  Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace RTIMULibCS
{
    internal static class I2CSupport
    {
        public static void Write(II2C device, byte reg, byte command, string exceptionMessage)
        {
            try
            {
                byte[] buffer = { reg, command };

                device.Write(buffer);
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }

        public static byte Read8Bits(II2C device, byte reg, string exceptionMessage)
        {
            try
            {
                byte[] addr = { reg };
                device.Write(addr);

                return device.Read(1)[0];
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }

        public static UInt16 Read16Bits(II2C device, byte reg, ByteOrder byteOrder, string exceptionMessage)
        {
            try
            {
                byte[] addr = { reg };
                device.Write(addr);

                byte[] data = device.Read(2);

                switch (byteOrder)
                {
                    case ByteOrder.BigEndian:
                        return (UInt16)((data[0] << 8) | data[1]);

                    case ByteOrder.LittleEndian:
                        return (UInt16)((data[1] << 8) | data[0]);

                    default:
                        throw new SensorException($"Unsupported byte order {byteOrder}");
                }
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }

        public static UInt32 Read24Bits(II2C device, byte reg, ByteOrder byteOrder, string exceptionMessage)
        {
            try
            {
                byte[] addr = { reg };
                device.Write(addr);

                byte[] data = device.Read(3);

                switch (byteOrder)
                {
                    case ByteOrder.BigEndian:
                        return (UInt32)((data[0] << 16) | (data[1] << 8) | data[2]);

                    case ByteOrder.LittleEndian:
                        return (UInt32)((data[2] << 16) | (data[1] << 8) | data[0]);

                    default:
                        throw new SensorException($"Unsupported byte order {byteOrder}");
                }
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }

        public static UInt32 Read32Bits(II2C device, byte reg, ByteOrder byteOrder, string exceptionMessage)
        {
            try
            {
                byte[] addr = { reg };
                device.Write(addr);

                byte[] data = device.Read(4);

                switch (byteOrder)
                {
                    case ByteOrder.BigEndian:
                        return (UInt32)((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | data[3]);

                    case ByteOrder.LittleEndian:
                        return (UInt32)((data[3] << 24) | (data[2] << 16) | (data[1] << 8) | data[0]);

                    default:
                        throw new SensorException($"Unsupported byte order {byteOrder}");
                }
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }

        public static byte[] ReadBytes(II2C device, byte reg, int count, string exceptionMessage)
        {
            try
            {
                byte[] addr = { reg };
                device.Write(addr);

                return device.Read(count);
            }
            catch (Exception exception)
            {
                throw new SensorException(exceptionMessage, exception);
            }
        }
    }
}
