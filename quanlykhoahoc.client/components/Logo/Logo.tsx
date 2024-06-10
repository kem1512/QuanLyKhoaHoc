import { Text } from "@mantine/core";
import Link from "next/link";

export default function Logo() {
  return (
    <Link
      href={"/"}
      style={{
        textDecoration: " none",
      }}
    >
      <Text
        fw={"bold"}
        color="light-dark(var(--mantine-color-gray-7), var(--mantine-color-dark-1))"
      >
        {process.env.NEXT_PUBLIC_WEBSITE_DESCRIPTION}
      </Text>
    </Link>
  );
}
