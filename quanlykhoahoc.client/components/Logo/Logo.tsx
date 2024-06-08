import { Text } from "@mantine/core";
import Link from "next/link";

export default function Logo() {
  return (
    <Link href={"/"} style={{ textDecoration: " none" }}>
      <Text fw={"bold"} color="dark">
        Quản Lý Khóa Học
      </Text>
    </Link>
  );
}
