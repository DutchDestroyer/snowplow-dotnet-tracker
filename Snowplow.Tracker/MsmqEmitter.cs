﻿/*
 * MsmqEmitter.cs
 * 
 * Copyright (c) 2014 Snowplow Analytics Ltd. All rights reserved.
 * This program is licensed to you under the Apache License Version 2.0,
 * and you may not use this file except in compliance with the Apache License
 * Version 2.0. You may obtain a copy of the Apache License Version 2.0 at
 * http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the Apache License Version 2.0 is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the Apache License Version 2.0 for the specific
 * language governing permissions and limitations there under.
 * Authors: Fred Blundun
 * Copyright: Copyright (c) 2014 Snowplow Analytics Ltd
 * License: Apache License Version 2.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Web.Script.Serialization;

namespace Snowplow.Tracker
{

    public class MsmqEmitter : IEmitter
    {
        private MessageQueue queue;

        public MsmqEmitter(string path = @".\private$\SnowplowTracker")
        {
            this.queue = MessageQueue.Exists(path) ? new MessageQueue(path) : MessageQueue.Create(path);
        }

        public void Input(Dictionary<string, string> payload)
        {
            this.queue.Send(new JavaScriptSerializer(null).Serialize(payload));
        }

        public void Flush(bool sync = false)
        {

        }
    }
}
