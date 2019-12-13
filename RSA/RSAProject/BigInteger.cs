using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class BigInteger
    {

        public BigInteger() { }

        public string Add(string x, string y)   //O(N)
        {
            //---------------- IF X > Result THEN COMPLETE Result BY ZEROS
            if (x.Length > y.Length)      //O(1)*O(N)=O(N)
            {
                CompleteWithZeros(ref y, y.Length, x.Length); //O(N)
            }
            //----------------ELSE IF X < Result THEN COMPLETE X BY ZEROS
            else if (x.Length < y.Length) //O(1)*O(N)=O(N)
            {
                CompleteWithZeros(ref x, x.Length, y.Length);  //O(N)
            }
            ////////////same code of adding no change
            //---------------- IF X = Result THEN NORMAL ADDING WE KNOW BEFORE
                int carry = 0;                      //O(1)
                string sum = "";                    //O(1)
                int isum = 0;                    //O(1)
                for (int i = x.Length - 1; i >= 0; i--)  //O(N)*b(O(1)+O(1)+.....)=O(N)
                {
                    isum = int.Parse(x[i] + "") + int.Parse(y[i] + "") + carry;                    //O(1)
                    sum = (isum % 10).ToString() + sum;                    //O(1)
                    if (isum > 9)                    //O(1)
                        carry = 1;                    //O(1)
                    else
                        carry = 0;                    //O(1)
                }
                if (carry != 0)                    //O(1)
                    sum = carry.ToString() + sum;                    //O(1)
                return sum;               //O(1)
            
        }
        public string Mul(string s1, string s2)//T(N)=3T(N/2)+CN+C2=O(N^1.58)master method
        {
            if (s1.Length < s2.Length) //O(1)*O(N)=O(N)
            {
                while (s1.Length < s2.Length)
                    s1 = "0" + s1;
            }
            else               //O(N)
            {
                while (s2.Length < s1.Length)
                    s2 = "0" + s2;
            }

            if (s1.Length == s2.Length && s1.Length == 1)          //O(1)
            {
                long x = 1, y = 1;
                x = Int64.Parse(s1[0].ToString());
                y = Int64.Parse(s2[0].ToString());
                long answer = x * y;
                return answer.ToString();
            }

            string a = "", b = "", c = "", d = "";

            a = s1.Substring(0, (s1.Length) / 2); 
            b = s1.Substring(s1.Length / 2, s1.Length - (s1.Length / 2));

            c = s2.Substring(0, (s2.Length) / 2);
            d = s2.Substring(s2.Length / 2, s2.Length - (s2.Length / 2));

            string m1 = Mul(a, c);    //T(N/2)
            string m2 = Mul(b, d);    //T(N/2)
            string z = Mul(Add(a, b), Add(c, d)); //T()
            string w;//= subtract(z, Add(m1, m2));
            w = subtract(subtract(z, m1), m2);


            for (int i = 0; i < 2 * (s1.Length - s1.Length / 2); i++)
                m1 += "0";

            for (int i = 0; i < s1.Length - s1.Length / 2; i++)
                w += "0";

            string ans = Add(Add(m1, m2), w);
            ans = deletestringzero(ans);

            return ans;
        }
        public void div(string a, string b, ref string q, ref string r)
        {
            if (b == "0")
            {
                DivideByZeroException ze = new DivideByZeroException();
                throw ze;
            }
            if (sign(a, b))
            {
                q = "0";
                r = a;
                return;
            }


            div(a, Add(b, b), ref q, ref r);
            q = Add(q, q);
            if (sign(r, b))
            {
                return;
            }

            else
            {
                q = Add(q, "1");
                r = subtract(r, b);
                return;
            }


        }
        static bool sign(string a, string b)//O(N)
        {
            int sz1 = a.Length, sz2 = b.Length;

            if (sz1 < sz2)
                return true;
            if (sz2 < sz1)
                return false;

            for (int i = 0; i < sz1; i++)
            {
                if (a[i] < b[i])
                    return true;

                else if (a[i] > b[i])
                    return false;
            }
            return false;
        }
        
        public string deletestringzero(string a) //O(N)
        {
            int cnt = 0;
            for (int i = 0; i < a.Length; i++)//O(N)
            {
                if (a[i] != '0')
                    break;
                cnt++;
            }
            string final = "";

            for (int i = cnt; i < a.Length; i++)//O(N)
                final += a[i];

            return final;
        }

        public string reverseString(string a)//O(N)
        {
            char[] ans = a.ToCharArray();
            Array.Reverse(ans);        //O(N)
            return new string(ans);   //O(1)
        }

        public string subtract(string a, string b)  //O(N)
        {
            bool negative = sign(a, b);  //O(N)
            string ans = "";

            if (negative)//O(1)
            {
                string temp = a;
                a = b;
                b = temp;
            }

            int n1 = a.Length, n2 = b.Length;  //O(1)
            a = reverseString(a);             //O(N)
            b = reverseString(b);             //O(N)
            int carry = 0;         //O(1)

            for (int i = 0; i < n2; i++)  //O(N)
            {
                int sub = ((int)(a[i] - '0') - (int)(b[i] - '0') - carry);
                if (sub < 0)
                {
                    sub += 10;
                    carry = 1;
                }
                else
                    carry = 0;
                ans += (char)(sub + '0');
            }

            for (int i = n2; i < n1; i++)  //O(N)
            {
                int sub = ((int)(a[i] - '0') - carry);
                if (sub < 0)
                {
                    sub += 10;
                    carry = 1;
                }
                else
                    carry = 0;

                ans += (char)(sub + '0');
            }

            //char deleteleadingzero = '0';
            int cnt = 0;
            for (int i = ans.Length - 1; i > 0; i--)  //O(N)
            {
                if (ans[i] != (char)('0'))
                    break;
                else
                    cnt++;
            }

            //Console.WriteLine(cnt);
            string final = "";
            for (int i = 0; i < ans.Length - cnt; i++)//O(N)
                final += ans[i];
            /*if (negative)
                final += '-';*/
            final = reverseString(final);

            return final;         //O(1)
        }
        public  void CompleteWithZeros(ref string str, int Start, int end)   //O(N)
        {
            for (int i = Start; i <= end - 1; i++)   //O(N)
                str = "0" + str;               //O(1)
        }
        public  bool MulnDivResultSign(ref string x, ref string y) // No Need for this if Unsigned
        {
            if (x[0] == '-' && y[0] == '-')
            {
                x = x.Substring(1);
                y = y.Substring(1);
                return true;
            }
            else if (x[0] != '-' && y[0] != '-')
            {
                return true;
            }
            else
            {
                if (x[0] == '-')
                    x = x.Substring(1);
                else
                    y = y.Substring(1);
                return false;
            }

        }
        public string PowerMod(string num, string pow, string mod)
        {
            string Q = "", R = "";
            string Result;
            if (pow == "0") { return "1"; }
            if (num == "0") { return "0"; }

            //div(pow, "2", ref Q, ref R);
            int check = Convert.ToInt32(num[num.Length - 1] )/ 2;
            if (check== 0)                       //Result= PowerMod(num,pow/2,mod)
            {
                div(pow, "2", ref Q, ref R);   //mid = Q=pow /2   even
                Result = PowerMod(num, Q, mod);
                Result = Mul(Result, Result);                 //Result=Result*Result
                div(Result, mod, ref Q, ref R);     //Result=Result % mod
                Result = R;
            }
            else                   //    odd
            {
                div(num, mod, ref Q, ref R);
                Result = R;
                div(PowerMod(num, subtract(pow, "1"), mod), mod, ref Q, ref R);//Result= PowerMod(num,pow-1,mod)  
                div(Mul(Result, R), mod, ref Q, ref R);
                Result = R;
            }
            Result = Add(Result, mod);
            div(Result, mod, ref Q, ref R);  //return Result=(Result+mod)%mod
            return R;
        }
        
    }
}
