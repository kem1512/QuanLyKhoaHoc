"use client";

import { Container, Group, ActionIcon, rem, Text } from "@mantine/core";
import {
  IconBrandTwitter,
  IconBrandYoutube,
  IconBrandInstagram,
} from "@tabler/icons-react";
import classes from "./RootFooter.module.css";
import Link from "next/link";

export function RootFooter() {
  return (
    <div className={classes.footer}>
      <Container className={classes.inner} size={"xl"}>
        <Link href={"/"} style={{ textDecoration: " none" }}>
          <Text fw={"bold"} color="dark">
            Quản Lý Khóa Học
          </Text>
        </Link>
        <Group
          gap={0}
          className={classes.links}
          justify="flex-end"
          wrap="nowrap"
        >
          <ActionIcon size="lg" color="gray" variant="subtle">
            <IconBrandTwitter
              style={{ width: rem(18), height: rem(18) }}
              stroke={1.5}
            />
          </ActionIcon>
          <ActionIcon size="lg" color="gray" variant="subtle">
            <IconBrandYoutube
              style={{ width: rem(18), height: rem(18) }}
              stroke={1.5}
            />
          </ActionIcon>
          <ActionIcon size="lg" color="gray" variant="subtle">
            <IconBrandInstagram
              style={{ width: rem(18), height: rem(18) }}
              stroke={1.5}
            />
          </ActionIcon>
        </Group>
      </Container>
    </div>
  );
}
