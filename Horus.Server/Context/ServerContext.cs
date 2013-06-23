/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horus.Client.System;
using Horus.Server.Model;

namespace Horus.Server.Context
{
    class ServerContext
    {
        private static Dictionary<string, Session> serverSessions = new Dictionary<string, Session>();
        private static Dictionary<string, Dictionary<string, object>> sessionObjects = new Dictionary<string, Dictionary<string, object>>();
        private static object syncRoot = new object();

        public static ServerContext Instance = new ServerContext();

        private ServerContext()
        { }

        public void AddSession(Session session, string sessionId)
        {
            lock (syncRoot)
            {
                if (!serverSessions.ContainsKey(sessionId))
                {
                    serverSessions.Add(sessionId, session);
                    sessionObjects.Add(sessionId, new Dictionary<string, object>());
                }
                else
                {
                    // Session already added. Is this an error
                }                
            }
        }

        public Session GetSession(string sessionId)
        {
            lock (syncRoot)
            {
                Session session;

                if (serverSessions.TryGetValue(sessionId, out session))
                    return session;

                return null;
            }
        }

        public Session GetSessionByUserId(string userId)
        {
            lock (syncRoot)
            {
                Session session = serverSessions.Values.SingleOrDefault(x => x.UserId == userId);

                return session;
            }
        }

        public void AddSessionObject(string sessionId, string objectId, object instance)
        {
            lock (syncRoot)
            {
                if (!sessionObjects[sessionId].ContainsKey(objectId))
                {
                    sessionObjects[sessionId].Add(objectId, instance);
                }
                else
                {
                    // TODO: Is this an error?
                }
            }
        }

        public object GetSessionObject(string sessionId, string objectId)
        {
            lock (syncRoot)
            {
                object instance;
                if (sessionObjects[sessionId].TryGetValue(objectId, out instance))
                    return instance;

                return null;
            }
        }
    }
}
