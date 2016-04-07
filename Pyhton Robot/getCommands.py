import urllib2
import json

def GetCommands():
    with open('configuration.txt') as data_file:
        configuration = json.load(data_file)
    id = configuration["RobotId"]

    getProgramsUrl = "http://robstark.azurewebsites.net/api/GetCommands?robotId=" + str(id)
    response = urllib2.urlopen(getProgramsUrl).read()

    f = open('commands.txt', 'w')
    f.write(str(response))
    f.close()

    with open('commands.txt') as data_file:
        json.load(data_file)

