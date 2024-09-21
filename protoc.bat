@echo off
protoc.exe --csharp_out="../Server/Model/Module/Message/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" ETServerOuterMessage.proto
protoc.exe --csharp_out="../Server/Hotfix/Module/Message/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" ETServerHotfixMessage.proto
protoc.exe --csharp_out="./Assets/GameMain/Scripts/NetWork/ETNetwork/MessageOutput/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" ClientHotfixMessage.proto
protoc.exe --csharp_out="./Assets/GameMain/Scripts/NetWork/ETNetwork/MessageOutput/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" ConFirmMsgBody.proto
protoc.exe --csharp_out="./Assets/GameMain/Scripts/NetWork/ETNetwork/MessageOutput/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" CommonBody.proto
protoc.exe --csharp_out="./Assets/GameMain/Scripts/NetWork/ETNetwork/MessageOutput/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" PlayerMsgBody.proto
protoc.exe --csharp_out="./Assets/GameMain/Scripts/NetWork/ETNetwork/MessageOutput/" --proto_path="./Assets/GameMain/Configs/ProtoDefine/" HeartbeatMsgBody.proto
echo generate message success,please close the console.
pause