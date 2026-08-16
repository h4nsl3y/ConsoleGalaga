using BusinessLayer.Models.GameModel.Characters.Vessels;

namespace BusinessLayer.Factory.CharacterFactory.VesselFactory
{
    public class VesselFactory((int min, int max) hBorder, (int min, int max) vBorder) : ICharacterFactory<IVessel>
    {
        #region Fields
        private const int DefaultHealth = 3;
        #endregion

        #region Public Methods
        public IVessel Generate()
        {
            return new Vessel(DefaultHealth, hBorder, vBorder);
        }
        #endregion
    }
}