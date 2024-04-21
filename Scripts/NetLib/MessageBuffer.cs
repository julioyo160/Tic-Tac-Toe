using System;
public class MessageBuffer
{
    private int _currentOffset;
    public byte[] Buffer { get; }
    public bool IsComplete => _currentOffset == Buffer.Length;
    public int Length => Buffer.Length;
    public MessageBuffer(int size)
    {
        Buffer = new byte[size];
    }
    public void Append(byte[] source, int length = -1)
    {
        var len = length > 0 ? length : source.Length;
        Array.Copy(source, 0, Buffer, _currentOffset, len);
        _currentOffset += len;
    }
}