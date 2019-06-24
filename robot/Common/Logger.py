#!/usr/bin/env python
# -*- coding: utf-8 -*-


import os
from datetime import datetime
import logging
import logging.handlers

rq = datetime.now().strftime('%Y-%m-%d_%H-%M-%S-%f')


class Logger(object):
    """Custom Logger for RobotTest

    both console and file will show log,
    log file name sample [log/2018-04-11_10-48-37-106000.log],
    """

    def __init__(self, name):
        """constructor for Logger

        Init Logger for Simplly use.

        Logger Formatter could change according to follow table.
        asctime     %(asctime)s
            Human-readable time when the LogRecord was created.
            By default this is of the form ‘2003-07-08 16:49:45,896’
            (the numbers after the comma are millisecond portion of the time).
        created     %(created)f
            Time when the LogRecord was created (as returned by time.time()).
        filename    %(filename)s
            Filename portion of pathname.
        funcName    %(funcName)s
            Name of function containing the logging call.
        levelname   %(levelname)s
            Text logging level for the message
            ('DEBUG', 'INFO', 'WARNING', 'ERROR', 'CRITICAL').
        levelno     %(levelno)s
            Numeric logging level for the message
            (DEBUG, INFO, WARNING, ERROR, CRITICAL).
        lineno      %(lineno)d
            Source line number where the logging call
            was issued (if available).
        module      %(module)s
            Module (name portion of filename).
        msecs       %(msecs)d
            Millisecond portion of the time when the LogRecord was created.
        message     %(message)s
            The logged message, computed as msg % args.
            This is set when Formatter.format() is invoked.
        name        %(name)s
            Name of the logger used to log the call.
        pathname    %(pathname)s
            Full pathname of the source file where the logging
            call was issued (if available).
        process     %(process)d
            Process ID (if available).
        processName %(processName)s
            Process name (if available).
        relativeCreated %(relativeCreated)d
            Time in milliseconds when the LogRecord was created,
            relative to the time the logging module was loaded.
        thread      %(thread)d
            Thread ID (if available).
        threadName  %(threadName)s
            Thread name (if available).

        Logger Could Use As:
            logger = Logger(__name__).get()
            logger.critical("Critical%d", 1, exc_info=1)
            logger.error("Error%d", 2, exc_info=1)
            logger.warning("Warning%d", 3, exc_info=1)
            logger.info("Info%d", 4, exc_info=1)
            logger.debug("Debug%d", 5, exc_info=1)

        msg         You shouldn’t need to format this yourself.
                    The format string passed in the original logging call.
                    Merged with args to produce message, or an arbitrary object
                    (see Using arbitrary objects as messages).
        args        You shouldn’t need to format this yourself.
                    The tuple of arguments merged into msg to produce message,
                    or a dict whose values are used for the merge
                    (when there is only one argument, and it is a dictionary).
        exc_info    You shouldn’t need to format this yourself.
                    Exception tuple (à la sys.exc_info) or,
                    if no exception has occurred, None.

        Arguments:
            name {str} -- Model Name For Logger
        """
        super(Logger, self).__init__()

        # init console handler of logger
        console_handler = logging.StreamHandler()
        console_handler_formatter = logging.Formatter(
            '%(asctime)s %(levelname)s - %(name)s\
[%(funcName)s:%(lineno)d] - %(message)s')
        console_handler.setFormatter(console_handler_formatter)

        # init file handler of logger
        path = "log"
        if not os.path.exists(path):
            os.makedirs(path)
        filename = path + '\\' + rq + '.log'
        file_handler = logging.FileHandler(filename)
        file_handler_formatter = logging.Formatter(
            '%(asctime)s %(levelname)s - %(name)s\
[%(funcName)s:%(lineno)d] - %(message)s')
        file_handler.setFormatter(file_handler_formatter)

        # init self.logger
        self.__logger = logging.getLogger(name)
        if not self.__logger.handlers:
            self.__logger.setLevel(logging.DEBUG)
            self.__logger.addHandler(console_handler)
            self.__logger.addHandler(file_handler)

    def get(self):
        return self.__logger


if __name__ == '__main__':
    logger = Logger(__name__).get()
    logger.critical("Critical%d", 1)
    logger.error("Error%d", 2)
    logger.warning("Warning%d", 3)
    logger.info("Info%d", 4)
    logger.debug("Debug%d", 5)
