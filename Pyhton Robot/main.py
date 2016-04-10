# -*- coding: utf-8 -*-
import threading
import json
import time

from enum import Enum
from api import registerRobot, getCommands
from executeCommands import executeServerCommands


class State(Enum):
    waitCommand = 1
    robotRegistered = 2
    programStarted = 3
    programFinished = 4

currentState = State.waitCommand

commandDescription = '\start - start application getting commands for Robot from Server' \
                     + '\n' +\
                     '\quit - finish this program' \
                     + '\n' +\
                     '\\register - registered robot in system'

class Command(Enum):
    start = 1
    register = 2
    quit = 3
    help = 4
    error = 5

def parseCommand(x):
    return {
        '\start': Command.start,
        '\\register': Command.register,
        '\quit': Command.quit,
        '\help' : Command.help
    }.get(x, Command.error)


class BackgroundCommandExecuter(object):
    def __init__(self, interval=5):
        self.interval = interval
        thread = threading.Thread(target=self.getCommandsFromServer, args=())
        thread.daemon = True
        thread.start()
    def getCommandsFromServer(self):
        while currentState == State.programStarted:
            getCommands()
            executeServerCommands()
            time.sleep(self.interval)

def onStart():
    global currentState
    if currentState == State.waitCommand:
        print "Error!\nPlease register robot first!"
    elif currentState == State.robotRegistered:
        currentState = State.programStarted
        BackgroundCommandExecuter()
    else:
        print 'unexpected error!\bplease report administrator!'

def onRegister():
    global currentState
    if (currentState == State.waitCommand):
        registerRobot()
        currentState = State.robotRegistered
    elif currentState == State.robotRegistered:
        print 'Error!\nRobot is already registered!'
    else:
        print 'unexpected error!\bplease report administrator!'

def onQuit():
    global currentState
    currentState = State.programFinished

def onHelp():
    print 'List of console commands:'
    print commandDescription

def onError():
    print "wrong command!\nPlease type '\help' to get help"

commandActions = {
    Command.start : onStart,
    Command.register : onRegister,
    Command.help : onHelp,
    Command.quit : onQuit,
    Command.error : onError()
}

def executeUserCommand(commandTxt):
    command = parseCommand(commandTxt)
    commandActions[command]()

def checkIfRobotRegistered():
    global currentState
    try:
        with open('configuration.txt') as data_file:
            configuration = json.load(data_file)
        configuration["RobotId"]
        currentState = State.robotRegistered
    except IOError:
        currentState = State.waitCommand


checkIfRobotRegistered()
while (currentState != State.programFinished):
    executeUserCommand(raw_input())
