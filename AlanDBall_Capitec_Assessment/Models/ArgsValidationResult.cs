using System;

namespace AlanDBall_Capitec_Assessment.Models
{
    [Flags]
    public enum ArgsValidationResult
    {
        Default = 0,
        ArgumentsNull = 1,
        UserTxtArgumentMissing = 2,
        TweetTxtArgumentMissing = 4,
        Ok = 5
    }
}
