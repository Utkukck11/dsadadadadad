using CoreCheat_Reborn.SDK.Managers;
using CoreCheat_Reborn.SDK.Controllers;
using static CoreCheat_Reborn.CheatClasses.Enums;
using static CoreCheat_Reborn.CheatClasses.Offsets.signatures;
using static CoreCheat_Reborn.CheatClasses.Offsets.others;

namespace CoreCheat_Reborn.CheatClasses
{
    class EngineClient
    {
        private static int GlowObject = CylMem.ReadInt(Modules.ClientDLLAdress + dwGlowObjectManager);
        private static int ClientStateBase = 0;
        public static void ConfigureClientState()
        {
            if(ClientStateBase == 0)
                ClientStateBase = CylMem.ReadInt(Modules.EngineDLLAdress + dwClientState);
        }
        public static int ClientState
        {
            get
            {
                if (ClientStateBase != 0)
                    return ClientStateBase;
                else
                {
                    ConfigureClientState();
                    return ClientStateBase;
                }
            }
        }
        public static int MaxPlayer
        {
            get
            {
                if (ClientStateBase != 0)
                    return CylMem.ReadInt(ClientState + dwClientState_MaxPlayer);
                else
                {
                    ConfigureClientState();
                    return CylMem.ReadInt(ClientState + dwClientState_MaxPlayer);
                }
            }
        }
        public static float GameTick
        {
            get
            {
                if (ClientStateBase != 0)
                    return CylMem.ReadFloat(Modules.EngineDLLAdress + dwGlobalVars + 16);
                else
                {
                    ConfigureClientState();
                    return CylMem.ReadFloat(Modules.EngineDLLAdress + dwGlobalVars + 16);
                }
            }
        }
        public static GameState GameState
        {
            get
            {
                if (ClientStateBase != 0)
                {
                    return (GameState)CylMem.ReadInt(ClientState + dwClientState_State);
                }
                else
                {
                    ConfigureClientState();
                    return (GameState)CylMem.ReadInt(ClientState + dwClientState_State);
                }
            }
        }
        public static ClassID GetClassId(int EntBase)
        {
            return (ClassID)CylMem.ReadInt(CylMem.ReadInt(CylMem.ReadInt(CylMem.ReadInt(EntBase + 8) + 2 * 4) + 1) + 20);
        }
        public static int GetObjectCount
        {
            get
            {
                if(ClientStateBase != 0)
                    return CylMem.ReadInt(GlowObject + 0x4);
                else
                {
                    ConfigureClientState();
                    return CylMem.ReadInt(GlowObject + 0x4);
                }
            }
        }
        public static int EntityCount
        {
            get
            {
                if (Modules.ClientDLLAdress != 0)
                {
                    var pCount = CylMem.ReadInt(Modules.ClientDLLAdress + dwEntityList + 0x24);
                    if (pCount != -1)
                        return pCount;
                    else
                        return MaxPlayer;
                }
                else
                    return 64;
            }
        }
        public static void ForceUpdate()
        {
            if(ClientStateBase != 0)
                CylMem.WriteInt(ClientState + clientstate_delta_ticks, -1);
            else
            {
                ConfigureClientState();
                CylMem.WriteInt(ClientState + clientstate_delta_ticks, -1);
            }
        }
        public static void ShowBombTrajectory()
        {
            if (ClientStateBase != 0)
                CylMem.CWrite<byte>(Modules.ClientDLLAdress + sv_grenade_trajectory, (byte)113);
            else
            {
                ConfigureClientState();
                CylMem.CWrite<byte>(Modules.ClientDLLAdress + sv_grenade_trajectory, (byte)113);
            }
        }
        public static void HideBombTrajectory()
        {
            if (ClientStateBase != 0)
                CylMem.CWrite<byte>(Modules.ClientDLLAdress + sv_grenade_trajectory, (byte)112);
            else
            {
                ConfigureClientState();
                CylMem.CWrite<byte>(Modules.ClientDLLAdress + sv_grenade_trajectory, (byte)112);
            }
        }
        public static byte BombTraject
        {
            get
            {
                return CylMem.CRead<byte>(Modules.ClientDLLAdress + sv_grenade_trajectory);
            }
        }
        public static bool SendPackets
        {
            set
            {
                CylMem.WriteBoolean(Modules.EngineDLLAdress + dwbSendPackets, value);
            }
        }
    }
}
