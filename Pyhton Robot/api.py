import requests
import json

# apiUrl = "http://robstark.azurewebsites.net/api/"
apiUrl = "http://localhost:45534/api/"

def registerRobot():
    registrationUrl = apiUrl + "RobotRegistration"
    response = requests.get(registrationUrl).text

    f = open('configuration.txt', 'w')
    f.write(str(response))
    f.close()

    with open('configuration.txt') as data_file:
        data = json.load(data_file)

    print 'Registered!\nRobot activation code is ' + str(data["ActivateCode"])

def getCommands():
    with open('configuration.txt') as data_file:
        configuration = json.load(data_file)
    id = configuration["RobotId"]

    getProgramsUrl = apiUrl + "GetCommands?robotId=" + str(id)
    response = requests.get(getProgramsUrl).text

    f = open('commands.txt', 'w')
    f.write(str(response))
    f.close()


def reportCommandGot(id):
    requests.post(apiUrl+"ReportCommandGot", data={'CommandId': str(id)})

def reportCommandExecuted(id):
    requests.post(apiUrl + "ReportCommandExecuted", data={'CommandId': str(id)})

def getProgram(id):
    getProgramUrl = apiUrl + "GetProgram?programId=" + str(id)
    response = requests.get(getProgramUrl).text
    return json.loads(response)

