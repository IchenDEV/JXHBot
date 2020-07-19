using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace com.zjutjh.jxhbot.Code
{
    public class JXHDic
    {
        private  static Dictionary<string, List<string>> Db = new Dictionary<string, List<string>>();

        public async void Update()
        {
            Db.Clear();
            HttpClient httpClient = new HttpClient();
            var ss = await (await httpClient.GetAsync("http://light.idevlab.cn/2000-2019-08-11.txt")).Content.ReadAsStringAsync();
            var collection = ss.Split(new string[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in collection)
            {
                var dic = item.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (dic.Length > 0)
                    Db.Add(dic[0], dic.Skip(1).ToList());
            }
        }

        public string Ans(string ori)
        {
            if (Db.ContainsKey(ori))
            {
                Random rs = new Random();
                return Db[ori][rs.Next(0, Db[ori].Count)].Replace("[换行]","\r\n");
            }
            return null;
        }
    }
}
