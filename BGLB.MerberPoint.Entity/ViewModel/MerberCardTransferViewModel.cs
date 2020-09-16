namespace BGLB.MerberPoint.Entity.ViewModel
{
    public class MerberCardTransferViewModel
    {
        public int UserId { get; set; }

        public string FromMerberCardId { get; set; }

        public string FromName { get; set; }

        public int FromPoint { get; set; }

        public float FromTotalMoney { get; set; }

        public string ToMerberCardId { get; set; }

        public string ToName { get; set; }

        public int ToPoint { get; set; }

        public float ToTotalMoney { get; set; }

        public string Remark { get; set; }

        public float TransferMoney{ get; set; }
    }
}
