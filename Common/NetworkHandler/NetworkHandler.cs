using Godot;
using System;

namespace NetworkMultiplayer
{
    public partial class NetworkHandler : Node
    {
        const string IP_ADDRESS = "localhost";
        const int PORT = 9999;

        public static NetworkHandler Instance {get; private set;}

        private ENetMultiplayerPeer peer;

        public override void _Ready()
        {
            Instance = this;
        }

        public void StartServer()
        {
            peer = new ENetMultiplayerPeer();
            peer.CreateServer(PORT);
            Multiplayer.MultiplayerPeer = peer;
        }

        public void StartClient()
        {
            peer = new ENetMultiplayerPeer();
            peer.CreateClient(IP_ADDRESS, PORT);
            Multiplayer.MultiplayerPeer = peer;
        }
    }
}
