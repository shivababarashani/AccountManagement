﻿namespace AccountManagement.ServicesProxy.Implementation.XProxy.Consts
{
    public static class XApiAddresses
    {
        public static string GetCustomerAccounts = "/Api/GetCustomerDepositslist";
        public static string GetAccountInfo = "/Api/GetAccountInfo";
        public static string GetCustomerInfo = "/Api/SearchRealCustomerByNationalCode";
        public static string BlockDeposit = "/BlockUnblock/BlockDeposit";
        public static string BlockWithdraw = "/BlockUnblock/BlockWithdraw";
        public static string MatchNationalCodeWithDepositNumber = "/Api/NationalCodeDepositNumberMatching";
        public static string AccountBalance = "/IntlMig/Customerdepositsamount";
        public static string InquiryDelegationStatus = "/ModernBanking/InquiryBrokerDepositNumber";
    }
}
