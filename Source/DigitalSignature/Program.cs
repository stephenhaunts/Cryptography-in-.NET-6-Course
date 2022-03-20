/*
MIT License

Copyright (c) 2022

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Pluralsight.Asymmetric;

static class Program
{
    static void Main()
    {
        SignAndVerifyData();

        Console.ReadLine();
    }

    private static void SignAndVerifyData()
    {
        var document = Encoding.UTF8.GetBytes("Document to Sign");

        var digitalSignature = new NewDigitalSignature();

        var signature = digitalSignature.SignData(document);

        var valid = digitalSignature.VerifySignature(signature.Item1, signature.Item2);

        Console.WriteLine(valid ? "The digital signature is VALID" : "The digital signature is INVALID");
    }
    
}