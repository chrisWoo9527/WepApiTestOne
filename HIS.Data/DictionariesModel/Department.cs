using HIS.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Data.DictionariesModel
{
    [Table(name: "ZD_KSFL")]
    public class Department
    {
        [Key]
        public long AID { get; set; }
        public int HosId { get; set; }
        public string KSBH { get; set; }

        public string? KSMC { get; set; }

        public string? SJKS { get; set; }

        public string? KSSX { get; set; }

        public decimal? YNBZC { get; set; }

        public decimal? SKC { get; set; }

        public decimal? BZC { get; set; }

        public decimal? TKC { get; set; }

        public string KZBZ { get; set; }

        public string? SFBZ { get; set; }

        public string? DTBZ { get; set; }

        public string? TYBZ { get; set; }

        public string? PYM { get; set; }

        public string? WBM { get; set; }

        public string? SZM { get; set; }

        public string? TEL_IN { get; set; }

        public string? TEL_OUT { get; set; }

        public string? KF_DM { get; set; }

        public string? CZY { get; set; }

        public DateTime? CZRQ { get; set; }

        public string? HostName { get; set; }

        public string? DISP_ORDER { get; set; }

        public string? DELFLAG { get; set; }

        public string? DELGH { get; set; }

        public DateTime? DELDATE { get; set; }

        public string? Zyxx { get; set; }

        public string? CJRY { get; set; }

        public DateTime? CJRQ { get; set; }

        public string? Send_Flag { get; set; }

        public string? OLDCODE { get; set; }

        public string? CIS_SEND { get; set; }

        public int? Isconfirmfee { get; set; }

        public int? DepInterType { get; set; }

        public string? Fyks { get; set; }

        public string? YBKSBM { get; set; }
    }
}
