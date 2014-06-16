using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class Mensaje
{
    public virtual string copde { get; set; }
    public virtual string type { get; set; }
    public virtual string language { get; set; }
    public virtual string name { get; set; }
    public virtual string value { get; set; }
    public virtual string comment { get; set; }
    public virtual string state { get; set; }

}
