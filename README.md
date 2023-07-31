# DeviceMonitor Project

## Requirement
To Monitor devices that are sending data to me
1. Each device has unique name
2. Each device sends measurement
3. Solution should be in C#

## Solution
A Web application that connects to MQTT Broker and subscribes to topics like temperature, humidity, and brightness sensors. Sensors publish data to the MQTT broker and a Web application that subscribes to the same topic will be retrieved and shown to the user.

## Design Architecture
![SolutionArchitecture](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/3df213a1-6d53-4387-a859-32ba1d544908)

### Technology Stack
1. HyperV - Virtualisation Server to Host OS
2. Home Assistant - Publisher with sensors
3. HiveMQ - MQTT Broker
4. Asp.net - Web Api
5. Vue - FrontEnd
6. HiveMQ.NET - .NET client subscriber to topics
7. C# - Programming Language

### Steps to Setup Environment
1. Home Assistant OS
  - Installation
    - Install HyperV using the link below
       - https://learn.microsoft.com/en-us/virtualization/hyper-v-on-windows/quick-start/enable-hyper-v
    - Install Home Assistant Disk Image using the link below
       - https://www.home-assistant.io/installation/windows
    - Post OS Install, Start the OS using HyperV Connect Button
       - ![Screenshot 2023-07-31 212147](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/192b062a-c576-4ee1-a624-84dd94048fb7)
    - Once the OS started, you can access them using http://homeassistant.local:8123/
  - Sensor Setup
    - Open the URL, Navigate to Settings->Add-Ons->Search for **File editor**-> select **File editor** and install
    - Once installed, File editor will be available in the URL as lefthand side menu.
    - Select the File Editor and open configuration yaml
         ![Screenshot 2023-07-31 213610](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/0568bbf6-7606-42e8-a68e-1aeb89d59885)
         ![configurationYaml](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/93820d40-6276-4e90-9247-ccc23917ec74)
    - Setup the Sensors Configuration like below

        
2. 
   
   
