namespace DailyPlanner
{
    class DailyReminder
    {
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string ActivationDate { get; private set; }

        // private DateTime ActivationHour { get; set; }
        // private bool Recurrence { get; set; }

        #region Setters
        public void SetShortDescription (string shortDescription)
        {
            this.ShortDescription = shortDescription;
        }

        public void SetDescription (string description)
        {
            this.Description = description;
        }

        public void SetActivationDate (string activationDate)
        {
            this.ActivationDate = activationDate;
        }
        #endregion

    }
}
