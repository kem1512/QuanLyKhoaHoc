import { Flex, Loader } from "@mantine/core";

export default function Loading() {
  return (
    <Flex justify={"center"} align={"center"}>
      <Loader size={"sm"}/>
    </Flex>
  );
}
