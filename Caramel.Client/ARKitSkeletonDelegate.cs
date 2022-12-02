using ARKit;
using Foundation;
using SceneKit;
using System;
using OpenTK;
using System.Collections.Generic;
using Caramel;
using CoreAnimation;
using UIKit;
using System.Net.Sockets;
using SharpOSC;

namespace Caramel.Client
{
    public class ARKitSkeletonDelegate : ARSCNViewDelegate
    {
        Dictionary<string, SCNNode> joints = new Dictionary<string, SCNNode>();

        public static UDPSender client;

        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (!(anchor is ARBodyAnchor bodyAnchor))
                return;

            foreach (var jointName in ARSkeletonDefinition.DefaultBody3DSkeletonDefinition.JointNames)
            {
                SCNNode jointNode = MakeJointGeometry(jointName);

                var jointPosition = GetDummyJointNode(bodyAnchor, jointName).Position;
                jointNode.Position = jointPosition;

                if (!joints.ContainsKey(jointName))
                {
                    node.AddChildNode(jointNode);
                    joints.Add(jointName, jointNode);
                }
            }
        }

        public async override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (!(anchor is ARBodyAnchor bodyAnchor))
                return;

            var jointNames = ARSkeletonDefinition.DefaultBody3DSkeletonDefinition.JointNames;
            
            foreach (string jointName in jointNames)
            {
                var jointPosition = GetDummyJointNode(bodyAnchor, jointName).Position;
                
                if (joints.ContainsKey(jointName))
                {
                    joints[jointName].Position = jointPosition;
                }
            }

            if(client != null)
            {
                var hipNode = GetDummyJointNode(bodyAnchor, "hips_joint");
                var lFootNode = GetDummyJointNode(bodyAnchor, "left_foot_joint");
                var rFootNode = GetDummyJointNode(bodyAnchor, "right_foot_joint");
                var headNode = GetDummyJointNode(bodyAnchor, "head_joint");

                await client.SendAsync(new OscMessage("/tracking/trackers/1/position", new object[] { -hipNode.Position.X, hipNode.Position.Y, hipNode.Position.Z }));
                await client.SendAsync(new OscMessage("/tracking/trackers/2/position", new object[] { -lFootNode.Position.X, lFootNode.Position.Y, lFootNode.Position.Z }));
                await client.SendAsync(new OscMessage("/tracking/trackers/3/position", new object[] { -rFootNode.Position.X, rFootNode.Position.Y, rFootNode.Position.Z }));
                await client.SendAsync(new OscMessage("/tracking/trackers/head/position", new object[] { -headNode.Position.X, headNode.Position.Y, headNode.Position.Z }));

                await client.SendAsync(new OscMessage("/tracking/trackers/1/rotation", new object[] { hipNode.EulerAngles.X.ToDegrees(), -hipNode.EulerAngles.Y.ToDegrees(), -hipNode.EulerAngles.Z.ToDegrees() }));
                await client.SendAsync(new OscMessage("/tracking/trackers/2/rotation", new object[] { lFootNode.EulerAngles.X.ToDegrees(), -lFootNode.EulerAngles.Y.ToDegrees(), -lFootNode.EulerAngles.Z.ToDegrees() }));
                await client.SendAsync(new OscMessage("/tracking/trackers/3/rotation", new object[] { rFootNode.EulerAngles.X.ToDegrees(), -rFootNode.EulerAngles.Y.ToDegrees(), -rFootNode.EulerAngles.Z.ToDegrees() }));
                await client.SendAsync(new OscMessage("/tracking/trackers/head/rotation", new object[] { headNode.EulerAngles.X.ToDegrees(), -headNode.EulerAngles.Y.ToDegrees(), -headNode.EulerAngles.Z.ToDegrees() }));

                hipNode.Dispose();
                lFootNode.Dispose();
                rFootNode.Dispose();
                headNode.Dispose();
            }
        }

        private SCNNode GetDummyJointNode(ARBodyAnchor bodyAnchor, string jointName)
        {
            NMatrix4 jointTransform = bodyAnchor.Skeleton.GetModelTransform((NSString)jointName);

            var node = new SCNNode()
            {
                Transform = jointTransform.ToSCNMatrix4(),
                Position = new SCNVector3(jointTransform.Column3)
            };

            return node;
        }

        private SCNNode MakeJointGeometry(string jointName)
        {
            var jointNode = new SCNNode();

            jointNode.Geometry = SCNSphere.Create(0.01f);
            ((SCNSphere)jointNode.Geometry).SegmentCount = 3;

            var material = new SCNMaterial();
            material.Diffuse.Contents = UIColor.Purple;
            jointNode.Geometry.FirstMaterial = material;

            return jointNode;
        }
    }

    public static class SceneKitExtensions
    {
        public static SCNMatrix4 ToSCNMatrix4(this NMatrix4 mtx)
        {
            return SCNMatrix4.CreateFromColumns(mtx.Column0, mtx.Column1, mtx.Column2, mtx.Column3);
        }

        public static float ToDegrees(this float radians)
        {
            float degrees = (180 / MathF.PI) * radians;
            return (degrees);
        }
    }
}

