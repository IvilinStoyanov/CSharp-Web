﻿using MishMash.Models.Enums;
using System.Collections.Generic;

namespace MishMash.Models
{
    public class Channel
    {
        public Channel()
        {
            this.Followers = new HashSet<UserChannel>();
            this.Tags = new HashSet<ChannelTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ChannelType Type { get; set; }

        public virtual ICollection<ChannelTag> Tags { get; set; }

        public virtual ICollection<UserChannel> Followers { get; set; }
    }
}
