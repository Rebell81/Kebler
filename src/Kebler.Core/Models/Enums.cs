﻿namespace Kebler.Core.Models
{
    public class Enums
    {
        public enum AddTorrentStatus
        {
            Added,
            Duplicate,
            UnknownError,
            ResponseNull,
            UpdatedTrackers
        }

        public enum Categories : byte
        {
            All,
            Downloading,
            Active,
            Stopped,
            Ended,
            Error,
            Inactive
        }

        public enum ErrorsResponse
        {
            TimeOut,
            HttpStatusCodeConflict,
            IsNotHttpWebResponse,
            None,
            SessionIdError,
            WebException
        }

        public enum FilterType
        {
            Name,
            Path
        }

        public enum MessageBoxDilogButtons
        {
            YesNo,
            YesNoCancel,
            OkCancel,
            Ok,
            None
        }

        public enum MessageBoxDilogButtonsResult
        {
            Yes,
            No,
            Ok,
            Cancel,
            None
        }

        public enum RemoveResult
        {
            Ok,
            Error,
            Cancel
        }

        public enum ReponseResult
        {
            Ok,
            NotOk
        }

        public enum ValidateError
        {
            Ok,
            TitileEmpty,
            TitileExists,
            IpOrHostError,
            PortError,
            RpcPathEmpty
        }
    }
}