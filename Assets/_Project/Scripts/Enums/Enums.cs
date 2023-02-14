
public enum AnimationState
{
    NONE,
    // Player unit
    BREATHINGIDLE = -240973668,
    SILLYDANCING = -890657550,
    // Stand unit
    SITTINGCLAP = -1053750580,
    SITTINGDISAPPROVAL = -145981872,
    SITTINGIDLE = 2028741046,
    SITTINGTALKING = -1039877749,
    SITTINGYELL = 1357620410,
    VICTORY = -1090111034,
    VICTORY2 = 1752031153
}

public enum AnimationFlags
{
    NONE,
    // Player unit
    BREATHINGIDLE = 1,
    SILLYDANCING = 2,
    // Stand unit
    SITTINGCLAP = 4,
    SITTINGDISAPPROVAL = 8,
    SITTINGIDLE = 16,
    SITTINGTALKING = 32,
    SITTINGYELL = 64,
    VICTORY = 128,
    VICTORY2 = 256
}

public enum LevelEndState
{
    WIN,
    LOSE,
    NEXT,
    PREV
}