# -*- coding: utf-8 -*-
import os
import json
import urllib2

getProgramUrl = "http://robstark.azurewebsites.net/api/GetProgram?programId="

def installProgram(id):
    response = urllib2.urlopen(getProgramUrl + str(id)).read()

    f = open('tmp.txt', 'w')
    f.write(str(response))
    f.close()

    with open('tmp.txt') as data_file:
        program = json.load(data_file)

    f = open('Programs\\' + str(id) + '_' + program["Name"] + '.txt', 'w')
    f.write(str(program["Code"]))
    f.close()
    return

def removeProgram(id):
    response = urllib2.urlopen(getProgramUrl + str(id)).read()

    f = open('tmp.txt', 'w')
    f.write(str(response))
    f.close()

    with open('tmp.txt') as data_file:
        program = json.load(data_file)

    os.remove('Programs\\' + str(id) + '_' + program["Name"] + '.txt')
    return

def parseCommand(command):
    if   str(command["Type"]) == "0":
        installProgram(command["Argument"])
    elif str(command["Type"]) == "1":
        removeProgram(command["Argument"])
        installProgram(command["Argument"])
    elif str(command["Type"]) == "2":
        removeProgram(command["Argument"])
    else:
        print "error"
    return

# создать папку если ее еще нет
if not os.path.exists('Programs'):
    os.makedirs('Programs')

# считать комманды
with open('commands.txt') as data_file:
    commands = json.load(data_file)

for command in commands:
    parseCommand(command)

