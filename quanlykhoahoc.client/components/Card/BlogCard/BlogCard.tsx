"use client";

import { Card, Image, Avatar, Text, Group } from "@mantine/core";
import classes from "./BlogCard.module.css";
import { BlogMapping } from "../../../app/web-api-client";

export function BlogCard({ data }: { data: BlogMapping }) {
  const { image, title, createTime, creator, id } = data;

  return (
    <Card withBorder radius="md" p={0} className={classes.card}>
      <Group wrap="nowrap" gap={0}>
        <Image alt={title} src={image} height={200} />
        <div className={classes.body}>
          <Text tt="uppercase" c="dimmed" fw={700} size="md">
            #{id}
          </Text>
          <Text className={classes.title} mt="xs" mb="md">
            {title}
          </Text>
          <Group wrap="nowrap" gap="xs">
            <Group gap="xs" wrap="nowrap">
              <Avatar size={20} src={creator.avatar} />
              <Text size="sm">{creator.username}</Text>
            </Group>
            <Text size="sm" c="dimmed">
              •
            </Text>
            <Text size="sm" c="dimmed">
              {createTime.toDateString()}
            </Text>
          </Group>
        </div>
      </Group>
    </Card>
  );
}
