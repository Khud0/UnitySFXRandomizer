# UnitySFXRandomizer

YouTube tutorial link: "NOT_UPLOADED_YET";

SFXPlayer class can:
1. Play a sound effect with a random pitch to add variation to the game.
2. Play a sound effect with its normal pitch.
3. Use any of the above methods on a random SFX from the provided array.

Extra notes:
1. SFXPlayer is a Singleton, hence only one instance of this class will exist in the game.
2. You don't need to use FindObjectOfType<SFXPlayer>() with this class, instead just type SFXPlayer.instance.