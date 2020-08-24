using CSR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace aaa
{
    class scr
    {
        public static void aaascr(MCCSAPI api)
        {
            Dictionary<string, string> uuid = new Dictionary<string, string>();
            api.addAfterActListener(EventKey.onLoadName, x =>
            {
                var a = BaseEvent.getFrom(x) as LoadNameEvent;
                uuid.Add(a.playername, a.uuid);
                return true;
            });
            api.addBeforeActListener(EventKey.onPlayerLeft, x =>
            {
                var a = BaseEvent.getFrom(x) as PlayerLeftEvent;
                uuid.Remove(a.playername);
                return true;
            });
            api.addBeforeActListener(EventKey.onUseItem, x =>
            {
                var a = BaseEvent.getFrom(x) as UseItemEvent;
                string obsidian = "minecraft:obsidian";
                string Bucket = "Bucket";
                //Console.WriteLine("{0} {1}", a.blockname, a.itemname);
                if (a.blockname == obsidian & a.itemname == Bucket & a.dimensionid == 0)
                {
                    api.runcmd("setblock " + a.position.x + " " + a.position.y + " " + a.position.z + " flowing_lava");
                    api.runcmd("tellraw " + a.playername + " {\"rawtext\":[{\"text\":\"§3[射爆庚子]§c已帮亲亲恢复岩浆了哦！\"}]}");

                }
                return true;
            });
            
 
        }
    }
}
namespace CSR
{
    partial class Plugin
    {

        public static void onStart(MCCSAPI api)
        {
            // TODO 此接口为必要实现
            aaa.scr.aaascr(api);
            Console.WriteLine("[爆射庚子]空岛小帮手已加载 作者_庚子 Soirks二改");
        }
    }
}