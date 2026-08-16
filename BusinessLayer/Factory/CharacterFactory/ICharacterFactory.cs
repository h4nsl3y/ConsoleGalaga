using BusinessLayer.Models.GameModel.Characters;

namespace BusinessLayer.Factory.CharacterFactory
{
    public interface ICharacterFactory<T> where T : ICharacter
    {
        T Generate();
    }
}
