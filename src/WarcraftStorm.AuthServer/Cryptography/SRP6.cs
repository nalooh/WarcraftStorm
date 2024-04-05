using System.Numerics;
using System.Security.Cryptography;

namespace WarcraftStorm.AuthServer.Cryptography;

public class SRP6 : IDisposable
{
    public readonly BigInteger g;

    public readonly BigInteger k;

    public readonly byte[] N;

    public readonly byte[] Salt;

    protected readonly SHA1 Sha1;

    protected BigInteger A;

    protected BigInteger BN;

    protected BigInteger v;

    protected BigInteger b;

    protected BigInteger s;

    public byte[] B { get; private set; }

    public byte[] K { get; private set; }

    public byte[] M2 { get; private set; }

    public byte[] V => v.ToByteArray();

    public SRP6()
    {
        Sha1 = new SHA1Managed();
        N =
        [
            137, 75,  100, 94,  137, 225, 83,  91,
            189, 173, 91,  139, 41,  6,   80,  83,
            8,   1,   177, 142, 191, 191, 94,  143,
            171, 60,  130, 135, 42,  62,  155, 183
        ];
        Salt =
        [
            173, 208, 58,  49,  210, 113, 20,  70,
            117, 242, 112, 126, 80,  38,  182, 210,
            241, 134, 89,  153, 118, 2,   80,  170,
            185, 69,  224, 158, 221, 42,  163, 69
        ];
        BN = MakeBigInteger(N);
        g = MakeBigInteger([ 7 ]);
        k = MakeBigInteger([ 3 ]);
    }

    public void CalculateX(byte[] passwordHash)
    {
        BigInteger x = MakeBigInteger(Sha1.ComputeHash(CombineData(Salt, passwordHash)));
        CalculateV(x);
    }

    private void CalculateV(BigInteger x)
    {
        v = BigInteger.ModPow(g, x, BN);
        CalculateB();
    }

    private void CalculateB()
    {
        byte[] data = new byte[20];
        RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(data);
        b = MakeBigInteger(data);
        B = GetBytes(((k * v + BigInteger.ModPow(g, b, BN)) % BN).ToByteArray(), 32);
    }

    public void CalculateU(byte[] a)
    {
        A = MakeBigInteger(a);
        CalculateS(MakeBigInteger(Sha1.ComputeHash(CombineData(a, B))));
    }

    private void CalculateS(BigInteger u)
    {
        s = BigInteger.ModPow(A * BigInteger.ModPow(v, u, BN), b, BN);
        CalculateK();
    }

    public void CalculateK()
    {
        byte[] bytes = this.GetBytes(this.s.ToByteArray(), 32);
        byte[] array = new byte[bytes.Length / 2];
        byte[] array2 = new byte[bytes.Length / 2];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = bytes[i * 2];
            array2[i] = bytes[i * 2 + 1];
        }
        array = this.Sha1.ComputeHash(array);
        array2 = this.Sha1.ComputeHash(array2);
        this.K = new byte[array.Length + array2.Length];
        for (int j = 0; j < array.Length; j++)
        {
            this.K[j * 2] = array[j];
            this.K[j * 2 + 1] = array2[j];
        }
    }

    public void CalculateM2(byte[] m1)
    {
        this.M2 = this.Sha1.ComputeHash(this.CombineData(this.CombineData(this.GetBytes(this.A.ToByteArray(), 32), m1), this.K));
    }

    public byte[] GetBytes(byte[] data, int count = 32)
    {
        if (data.Length == count)
        {
            return data;
        }
        byte[] array = new byte[count];
        Buffer.BlockCopy(data, 0, array, 0, 32);
        return array;
    }

    public BigInteger MakeBigInteger(byte[] data)
    {
        byte[] data2 = new byte[1];
        return new BigInteger(this.CombineData(data, data2));
    }

    public byte[] CombineData(byte[] data, byte[] data2)
    {
        return new byte[0].Concat(data).Concat(data2).ToArray<byte>();
    }

    public void Dispose()
    {
        this.K = null;
        this.M2 = null;
    }
    
}
