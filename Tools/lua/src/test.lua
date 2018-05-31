package.path = ".\\src\\?.lua;"..package.path

require "protobuf"

function register( path )
	local protofile = io.open(path, "rb")
	local buffer = protofile:read "*a"
	protofile:close()

	protobuf.register(buffer)

	local t = protobuf.decode("google.protobuf.FileDescriptorSet", buffer)

	local proto = t.file[1]

	print(proto.name)
	print(proto.package)

	local message = proto.message_type

	for _,v in ipairs(message) do
		print(v.name)
		for _,v in ipairs(v.field) do
			print("\t".. v.name .. " ["..v.number.."] " .. v.label)
		end
	end
end

register( "../../../Test/lua/proto/GPCommon.pbc" )
register( "../../../Test/lua/proto/GPTSample.pbc" )

local transform = {
	position = {
		x = 1,
		y = 2,
		z = 3
	},
	name = "哈哈哈"
}

local code = protobuf.encode("GP.Table.Transform", transform);

local datafile = io.open("../../../Test/lua/proto/GPTSample.data", "wb")
datafile:write(code)
datafile:close()

datafile = io.open("../../../Test/lua/proto/GPTSample.data", "rb")
local databuffer = datafile:read "*a"
datafile:close()

local decode = protobuf.decode("GP.Table.Transform" , databuffer)
print(decode.name)
print(string.format("%s,%s,%s",decode.position.x,decode.position.y,decode.position.z))

-- addressbook = {
-- 	name = "Alice",
-- 	id = 12345,
-- 	phone = {
-- 		{ number = "1301234567" },
-- 		{ number = "87654321", type = "WORK" },
-- 	}
-- }

-- code = protobuf.encode("tutorial.Person", addressbook)

-- decode = protobuf.decode("tutorial.Person" , code)

-- print(decode.name)
-- print(decode.id)
-- for _,v in ipairs(decode.phone) do
-- 	print("\t"..v.number, v.type)
-- end

-- phonebuf = protobuf.pack("tutorial.Person.PhoneNumber number","87654321")
-- buffer = protobuf.pack("tutorial.Person name id phone", "Alice", 123, { phonebuf })
-- print(protobuf.unpack("tutorial.Person name id phone", buffer))
