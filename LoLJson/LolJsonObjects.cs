using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LoLJson
{
    // ReSharper disable InconsistentNaming
    public class Position
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class PlayerFrame
    {
        [Key]
        public int id { get; set; }
        public int currentGold { get; set; }
        public Position position { get; set; }
        public int minionsKilled { get; set; }
        public int level { get; set; }
        public int jungleMinionsKilled { get; set; }
        public int totalGold { get; set; }
        public int dominionScore { get; set; }
        public int participantId { get; set; }
        public int xp { get; set; }
        public int teamScore { get; set; }
    }


    public class ParticipantFrames
    {
        [Key]
        public int id { get; set; }
        [JsonProperty("1")]
        public PlayerFrame Player1Frame { get; set; }
        [JsonProperty("2")]
        public PlayerFrame Player2Frame { get; set; }
        [JsonProperty("3")]
        public PlayerFrame Player3Frame { get; set; }
        [JsonProperty("4")]
        public PlayerFrame Player4Frame { get; set; }
        [JsonProperty("5")]
        public PlayerFrame Player5Frame { get; set; }
        [JsonProperty("6")]
        public PlayerFrame Player6Frame { get; set; }
        [JsonProperty("7")]
        public PlayerFrame Player7Frame { get; set; }
        [JsonProperty("8")]
        public PlayerFrame Player8Frame { get; set; }
        [JsonProperty("9")]
        public PlayerFrame Player9Frame { get; set; }
        [JsonProperty("10")]
        public PlayerFrame Player10Frame { get; set; }
    }


    public class Event
    {
        [Key]
        public int id { get; set; }
        public int timestamp { get; set; }
        public int participantId { get; set; }
        public string eventType { get; set; }
        public int itemId { get; set; }
        public string levelUpType { get; set; }
        public int? skillSlot { get; set; }
        public int? itemBefore { get; set; }
        public int? itemAfter { get; set; }
        public int? creatorId { get; set; }
        public string wardType { get; set; }
        public virtual Position position { get; set; }
        public string monsterType { get; set; }
        public int? killerId { get; set; }
        public int? victimId { get; set; }
        public virtual List<int?> assistingParticipantIds { get; set; }
        public string laneType { get; set; }
        public string buildingType { get; set; }
        public string towerType { get; set; }
        public int? teamId { get; set; }
    }

    public class Frame
    {
        [Key]
        public int id { get; set; }
        public int timestamp { get; set; }
        public ParticipantFrames participantFrames { get; set; }
        public List<Event> events { get; set; }
    }

    public class Timeline
    {
        [Key]
        public int id { get; set; }
        public int frameInterval { get; set; }
        public List<Frame> frames { get; set; }
    }

    public class Mastery
    {
        public int rank { get; set; }
        public int masteryId { get; set; }
    }

    public class Stats
    {
        public int unrealKills { get; set; }
        public int item2 { get; set; }
        public int item1 { get; set; }
        public int totalDamageTaken { get; set; }
        public int item0 { get; set; }
        public int pentaKills { get; set; }
        public int sightWardsBoughtInGame { get; set; }
        public bool winner { get; set; }
        public int magicDamageDealt { get; set; }
        public int wardsKilled { get; set; }
        public int largestCriticalStrike { get; set; }
        public int trueDamageDealt { get; set; }
        public int doubleKills { get; set; }
        public int physicalDamageDealt { get; set; }
        public int tripleKills { get; set; }
        public int deaths { get; set; }
        public bool firstBloodAssist { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int assists { get; set; }
        public int visionWardsBoughtInGame { get; set; }
        public int totalTimeCrowdControlDealt { get; set; }
        public int champLevel { get; set; }
        public int physicalDamageTaken { get; set; }
        public int totalDamageDealt { get; set; }
        public int largestKillingSpree { get; set; }
        public int inhibitorKills { get; set; }
        public int minionsKilled { get; set; }
        public int towerKills { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int quadraKills { get; set; }
        public int goldSpent { get; set; }
        public int totalDamageDealtToChampions { get; set; }
        public int goldEarned { get; set; }
        public int neutralMinionsKilledTeamJungle { get; set; }
        public bool firstBloodKill { get; set; }
        public bool firstTowerKill { get; set; }
        public int wardsPlaced { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int killingSprees { get; set; }
        public bool firstInhibitorKill { get; set; }
        public int totalScoreRank { get; set; }
        public int totalUnitsHealed { get; set; }
        public int kills { get; set; }
        public bool firstInhibitorAssist { get; set; }
        public int totalPlayerScore { get; set; }
        public int neutralMinionsKilledEnemyJungle { get; set; }
        public int magicDamageTaken { get; set; }
        public int largestMultiKill { get; set; }
        public int totalHeal { get; set; }
        public int item4 { get; set; }
        public int item3 { get; set; }
        public int objectivePlayerScore { get; set; }
        public int item6 { get; set; }
        public bool firstTowerAssist { get; set; }
        public int item5 { get; set; }
        public int trueDamageTaken { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int combatPlayerScore { get; set; }
    }

    public class Rune
    {
        public int rank { get; set; }
        public int runeId { get; set; }
    }

    public class XpDiffPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class DamageTakenDiffPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class XpPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class GoldPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class CreepsPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class CsDiffPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class DamageTakenPerMinDeltas
    {
        public double zeroToTen { get; set; }
        public double thirtyToEnd { get; set; }
        public double tenToTwenty { get; set; }
        public double twentyToThirty { get; set; }
    }

    public class PlayerTimeLine
    {
        [Key]
        public int id { get; set; }
        public virtual XpDiffPerMinDeltas xpDiffPerMinDeltas { get; set; }
        public virtual DamageTakenDiffPerMinDeltas damageTakenDiffPerMinDeltas { get; set; }
        public virtual XpPerMinDeltas xpPerMinDeltas { get; set; }
        public virtual GoldPerMinDeltas goldPerMinDeltas { get; set; }
        public string role { get; set; }
        public virtual CreepsPerMinDeltas creepsPerMinDeltas { get; set; }
        public virtual CsDiffPerMinDeltas csDiffPerMinDeltas { get; set; }
        public virtual DamageTakenPerMinDeltas damageTakenPerMinDeltas { get; set; }
        public string lane { get; set; }
    }

    public class Participant
    {
        public virtual List<Mastery> masteries { get; set; }
        public virtual Stats stats { get; set; }
        public virtual List<Rune> runes { get; set; }
        public virtual PlayerTimeLine timeline { get; set; }
        public int spell2Id { get; set; }
        public int participantId { get; set; }
        public int championId { get; set; }
        public int teamId { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public int spell1Id { get; set; }
    }

    public class Player
    {
        [Key]
        public int id { get; set; }
        public int profileIcon { get; set; }
        public string matchHistoryUri { get; set; }
        public string summonerName { get; set; }
        public int summonerId { get; set; }
    }

    public class ParticipantIdentity
    {
        public virtual Player player { get; set; }
        [Key]
        public int participantId { get; set; }
    }

    public class Ban
    {
        public int banId { get; set; }
        public int pickTurn { get; set; }
        public int championId { get; set; }
    }

    public class Team
    {
        public int inhibitorKills { get; set; }
        public int dominionVictoryScore { get; set; }
        public virtual List<Ban> bans { get; set; }
        public int towerKills { get; set; }
        public bool firstTower { get; set; }
        public bool firstBlood { get; set; }
        public bool firstBaron { get; set; }
        public bool firstInhibitor { get; set; }
        public bool firstDragon { get; set; }
        public bool winner { get; set; }
        public int vilemawKills { get; set; }
        public int baronKills { get; set; }
        public int dragonKills { get; set; }
        public int teamId { get; set; }
    }

    public class Match
    {
        public string region { get; set; }
        public string matchType { get; set; }
        public long matchCreation { get; set; }
        public virtual Timeline timeline { get; set; }
        public virtual List<Participant> participants { get; set; }
        public string platformId { get; set; }
        public string matchMode { get; set; }
        public virtual List<ParticipantIdentity> participantIdentities { get; set; }
        public string matchVersion { get; set; }
        public virtual List<Team> teams { get; set; }
        public int mapId { get; set; }
        public string season { get; set; }
        public string queueType { get; set; }
        public int matchId { get; set; }
        public int matchDuration { get; set; }
    }

    public class GamseToDownload
    {
        public List<int> games { get; set; }
    }
}
