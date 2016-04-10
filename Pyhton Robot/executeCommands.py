# -*- coding: utf-8 -*-
import os
import json
from enum import Enum

from api import getProgram, reportCommandExecuted, reportCommandGot

getProgramUrl = "http://robstark.azurewebsites.net/api/GetProgram?programId="

class ProgramAction(Enum):
    install = 0
    update = 1
    remove = 2
    error = 3

def parseAction(x):
    return {
        '0': ProgramAction.install,
        '1': ProgramAction.update,
        '2': ProgramAction.remove,
    }.get(x, ProgramAction.error)

def removeProgram(id):
    program = getProgram(id)
    f = open('Programs\\' + str(id) + '_' + program["Name"] + '.txt', 'w')
    f.write(str(program["Code"]))
    f.close()
    os.remove('Programs\\' + str(id) + '_' + program["Name"] + '.txt')

id = -1

def onInstallProgram():
    global id
    program = getProgram(id)
    f = open('Programs\\' + str(id) + '_' + program["Name"] + '.txt', 'w')
    f.write(str(program["Code"]))
    f.close()
    print 'program' + str(id) + ' was installed'

def onUpdateProgram():
    onRemoveProgram()
    onInstallProgram()
    print 'program' + str(id) + ' was updated'

def onRemoveProgram():
    global id
    removeProgram(id)
    print 'program' + str(id) + ' was removed'

def onErrorProgram():
    print 'error!\n wrong program operation type!'

programActions = {
    ProgramAction.install : onInstallProgram,
    ProgramAction.update : onUpdateProgram,
    ProgramAction.remove : onRemoveProgram,
    ProgramAction.error : onErrorProgram,
}

def executeServerCommands():
    global id
    if not os.path.exists('Programs'):
        os.makedirs('Programs')
    with open('commands.txt') as data_file:
        commands = json.load(data_file)
    for command in commands:
        action = parseAction(str(command["Type"]))
        id = command["Argument"]
        idCommand = command["RobotCommandID"]
        reportCommandGot(idCommand)
        programActions[action]()
        reportCommandExecuted(idCommand)



