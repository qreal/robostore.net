# -*- coding: utf-8 -*-
import threading
import json
from enum import Enum

from getCommands import GetCommands
from registration import RegisterRobot


class State(Enum):
    # ожидание входной комманды
    default = 1
    # зарегистрировать Робота в системе
    registered = 2
    # включить запрос комманд с сервера по таймеру и их выполнение
    started = 3
    # завершить работу программы
    finished = 4

currentState = State.default

commandDescription = '\start - start application getting commands for Robot from Server' \
                     + '\n' +\
                     '\quit - finish this program' \
                     + '\n' +\
                     '\\register - registered robot in system'


# начинаем или прекращаем получать комманды от сервера
# в зависимости от текущего состояния
def checkNewCommands():
    global timer
    GetCommands()
    if currentState == State.started:
        timer.start()
    elif currentState == State.finished:
        timer.cancel()
        
timer = threading.Timer(60, checkNewCommands)

def parseCommand(command):
    global currentState
    if command == "\start":
        if currentState == State.default:
            print "Error!\nPlease register robot first!"
        elif currentState == State.registered:
            currentState = State.started
            checkNewCommands()
        else:
            print 'unexpected error!\bplease report administrator!'
    elif command == "\\register":
        RegisterRobot()
        if (currentState == State.default):
            currentState = State.registered
        elif currentState == State.registered:
            print 'Error!\nRobot is already registered!'
        else:
            print 'unexpected error!\bplease report administrator!'
    elif command == "\quit":
        currentState = State.finished
        checkNewCommands()
    elif command == "\help":
        print 'List of console commands:'
        print commandDescription
    else:
        print "wrong command!\nPlease type '\help' to get help"

# попробовать считать код активации и перейти в нужное состояние
try:
    with open('configuration.txt') as data_file:
        configuration = json.load(data_file)
    id = configuration["RobotId"]
    currentState = State.registered
except IOError:
    currentState = State.default

while (currentState != State.finished):
    parseCommand(raw_input())
