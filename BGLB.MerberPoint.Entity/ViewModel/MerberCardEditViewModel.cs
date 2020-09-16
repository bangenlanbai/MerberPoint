using BGLB.MerberPoint.Common;
using System;

namespace BGLB.MerberPoint.Entity.ViewModel
{
    public class MerberCardEditViewModel
    {

        public int Id { get; set; }

        public string CardId { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }


        public string PasswordConfim { get; set; }

        public SexTypeEnum Sex { get; set; }

        public int CardLevelId { get; set; }

        public int BirthdayType { get; set; }

        public int BirthdayMonth { get; set; }

        public int BirthdayDay { get; set; }

        public bool IsPast { get; set; }

        public DateTime PastTime { get; set; }

        public CardStateTypeEnum CardState { get; set; }

        public decimal Money { get; set; }

        public int Point { get; set; }

        public bool IsPointAuto { get; set; }

        public string RefererCard { get; set; }

        public string RefererName { get; set; }
        public int State { get; set; }
    }
}
