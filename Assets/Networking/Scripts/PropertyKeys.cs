namespace Properties {
    public static class RoomProps {
        /// <summary>
        /// currently ongoing turn number
        /// </summary>
        public static readonly string TurnPropKey = "Turn";

        /// <summary>
        /// start (server) time for currently ongoing turn (used to calculate end)
        /// </summary>
        public static readonly string TurnStartTimePropKey = "TStart";

        /// <summary>
        /// Finished Turn of Actor (followed by number)
        /// </summary>
        public static readonly string FinishedTurnPropKey = "FToA";

        /// <summary>
        /// get which players turn it is
        /// </summary>
        public static readonly string ActivePlayerPropKey = "ActivePlayer";

        /// <summary>
        /// Stores the game state of the room
        /// </summary>
        public static readonly string CurrentGameState = nameof(CurrentGameState);
    }

    public static class PlayerProps {
        /// <summary>
        /// Team number of the player
        /// </summary>
        public static readonly string Team = nameof(Team);

        /// <summary>
        /// returns a bool on whether deck is empty
        /// </summary>
        public static readonly string EmptyDeck = nameof(EmptyDeck);
    }
}