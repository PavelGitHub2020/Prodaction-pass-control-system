using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicClassesLibrary.BLL
{
    public interface IAudioMessageAboutTimeIsNotSet
    {
        void SoundMessageAboutTimeIsNotSet(System.Media.SoundPlayer player, bool soundState, string languageState);
    }
}
