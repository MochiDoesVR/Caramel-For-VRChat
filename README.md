# Caramel-For-VRChat

A simple full-body tracking solution for VRChat using Apple's ARKit

## Setup

1. Download the latest `.ipa` from [Releases](https://github.com/MochiDoesVR/Caramel-For-VRChat/releases)
2. Sideload the app using something like [Altstore](https://altstore.io)
3. Start the OSC receiver app (e.g. VRChat) and enable the OSC API
4. Make sure that both your iPhone and the receiver host (e.g. a Meta Quest) are on the same network
5. Launch Caramel, open the `Settings` tab and put in the local IP of the receiver AND the port (`9000` by default for VRChat)
6. Go back to the preview screen
7. If using VRChat, open the OSC Debug console and verify that it is receiving data from Caramel
