"use client";

import { IconShoppingCartShare } from "@tabler/icons-react";
import {
  Card,
  Image,
  Text,
  Group,
  Badge,
  Button,
  ActionIcon,
} from "@mantine/core";
import classes from "./CourseCard.module.css";
import { CourseMapping } from "../../../app/web-api-client";
import { formatCurrencyVND } from "../../../lib/helper";
import Link from "next/link";

export function CourseCard({ data }: { data: CourseMapping }) {
  const { id, code, imageCourse, name, price, introduce, courseSubjects } = data;

  const subjects = courseSubjects.map((badge) => (
    <Badge
      variant="light"
      key={badge.subject.id}
      leftSection={badge.subject.symbol}
    >
      {badge.subject.name}
    </Badge>
  ));

  return (
    <Card withBorder radius="md" p="md" className={classes.card}>
      <Card.Section>
        <Image src={imageCourse} alt={imageCourse} height={250} />
      </Card.Section>

      <Card.Section className={classes.section} mt="md">
        <Group justify="apart">
          <Link href={`/${id}`} style={{ textDecoration: "none" }}>
            <Text fz="lg" fw={500}>
              {name}
            </Text>
          </Link>
          <Badge size="sm" variant="light">
            {formatCurrencyVND(price)}
          </Badge>
        </Group>
        <Text fz="sm" mt="xs">
          {introduce}
        </Text>
      </Card.Section>

      <Group mt="xs">
        <Link href={`/${id}`} style={{ flex: 1 }}>
          <Button radius="md" size="xs" w={"100%"}>
            Xem Chi Tiết
          </Button>
        </Link>
        <ActionIcon variant="default" radius="md" size={36}>
          <IconShoppingCartShare className={classes.like} stroke={1.5} />
        </ActionIcon>
      </Group>
    </Card>
  );
}
