using System;



namespace TraineeHelperNew.Dal
{
    interface IMongoCommon : IMongoEntity<ObjectId>
    {
        string Name { get; set; }
        bool IsActive { get; set; }
        string Description { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
