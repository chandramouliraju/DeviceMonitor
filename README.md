# DeviceMonitor Project

## Requirement
To Monitor devices that are sending data to me
1. Each device has a unique name
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

### Steps to Setup Environment - Windows OS
- Home Assistant OS
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
       - ![Screenshot 2023-07-31 213610](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/0568bbf6-7606-42e8-a68e-1aeb89d59885)
    - Select the File Editor and open configuration yaml
      - ![configurationYaml](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/93820d40-6276-4e90-9247-ccc23917ec74)
    - Setup the Sensors Configuration like the configuration.yaml available in the Github project
      - ![sensors](https://github.com/chandramouliraju/DeviceMonitor/assets/32584364/3b3456c6-9632-43ab-90d8-b9c1ab3824e4 
  - Broker Configuration - Post Step 2
    - Setup the MQTT Broker using Link below
        - https://www.home-assistant.io/integrations/mqtt/
- HiveMQTT
   - Download - https://www.hivemq.com/downloads/hivemq/
      - Start Server
        - Unzip HiveMQ zip
        - open bin folder
        - right click **run.bat** and **run as administrator**
        - Broker should be installed in port 1883 by default
      - Follow Broker Configuration in Step 1
- Configure Topic
  - Goto URL - http://homeassistant.local:8123/
    - Select **Settings** on lefthand side menu
    - Select **Devices & Services** on the dashboard
    - Select **MQTT** on the **Integrations** tab
    - Select **Configure** under **Integration entries**
    - Publish a Packet - Temperature Sensor
      - Topic - **home/sensor/temperature**
      - Select **Allow Template** - This is to get the functions to get device id and unit of measurement to automatically populate from configuration.yaml and timestamp from system
      - Payload -
          **{
            "measurement":"25",
            "unitOfMeasurement": "{{ state_attr('sensor.temperature','unit_of_measurement') }}",
            "deviceId": "{{ device_id('sensor.temperature') }}",
            "dateTime": "{{ now().isoformat()  }}"
           }**
    - Publish a Packet - Humidity Sensor
      - Topic - **home/sensor/humidity**
      - Select **Allow Template** - This is to get the functions to get device id and unit of measurement to automatically populate from configuration.yaml and timestamp from system
      - Payload -
          **{
            "measurement":"25",
            "unitOfMeasurement": "{{ state_attr('sensor.humidity','unit_of_measurement') }}",
            "deviceId": "{{ device_id('sensor.humidity') }}",
            "dateTime": "{{ now().isoformat()  }}"
           }**
     - Publish a Packet - Brightness Sensor
      - Topic - **home/sensor/brightness**
      - Select **Allow Template** - This is to get the functions to get device id and unit of measurement to automatically populate from configuration.yaml and timestamp from system
      - Payload -
          **{
            "measurement":"25",
            "unitOfMeasurement": "{{ state_attr('sensor.brightness','unit_of_measurement') }}",
            "deviceId": "{{ device_id('sensor.brightness') }}",
            "dateTime": "{{ now().isoformat()  }}"
           }**
     - You can check the published topics are printing the correct data by start listening to the same topic
         - Under **Listen to a topic**
         - Enter the topic to subscribe of any one from above 3 topics
         - Select **Start Listening**
- Webapp - to Monitor the messages in browser
     - Checkout the code from branch feature/develop
     - Open the DeviceMonitor.sln using Visual Studio
     - Make sure the startup projects are chosen as Multiple Projects Both WebApi and vueapp
     - Press Start
     - Publish the packet from step 3
     - Refresh the Webapp to get the latest message published.
    
    
    
     
