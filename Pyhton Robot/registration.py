import urllib2
import json

registrationUrl = "http://robstark.azurewebsites.net/api/RobotRegistration"
response = urllib2.urlopen(registrationUrl).read()

f = open('configuration.txt', 'w')
f.write(str(response))
f.close()

with open('configuration.txt') as data_file:
    data = json.load(data_file)

print data["ActivateCode"]