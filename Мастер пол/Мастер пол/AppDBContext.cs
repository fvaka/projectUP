using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace Мастер_пол
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext() : base("name=dbMasterFloorUPEntities") { }

        public DbSet<Партнеры> партнерыs { get; set; }
        public DbSet<Заявки> заявкиs { get; set; }
        public DbSet<История_продаж_партнеров> история_Продажs { get; set; }
        public DbSet<Сотрудники> сотрудникиs { get; set; }
        public DbSet<Продукция> продукцияs{ get; set; }
        public DbSet<Материалы> материалыs{ get; set; }
        public DbSet<Элементы_заявок> элементы_Заявокs{ get; set; }
        public DbSet<Материалы_для_продукции> Материалы_Для_Продукцииs{ get; set; }
        public DbSet<Поставщики> поставщикиs{ get; set; }

    }
}
