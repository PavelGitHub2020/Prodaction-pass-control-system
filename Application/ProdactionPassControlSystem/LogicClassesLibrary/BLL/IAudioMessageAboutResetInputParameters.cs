using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    public interface IAudioMessageAboutResetInputParameters
    {
        void SoundMessageAboutResetInputParameters(System.Media.SoundPlayer player, bool soundState, string languageState);
    }
}
