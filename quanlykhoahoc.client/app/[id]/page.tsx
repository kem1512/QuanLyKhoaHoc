"use client";

import { Button, Grid, Group, Image, Text, Title } from "@mantine/core";
import RootLayout from "../../components/Layout/RootLayout/RootLayout";
import useSWR from "swr";
import { CourseClient } from "../web-api-client";
import { formatCurrencyVND } from "../../lib/helper";

export default function Detail({ params }: { params: { id: number } }) {
  const { id } = params;

  const { data } = useSWR(`/api/course/${id}`, () =>
    new CourseClient().getEntity(id)
  );

  return (
    <RootLayout>
      <Grid py={"md"}>
        <Grid.Col span={{ base: 12, lg: 8 }}>
          <Title order={2} mb="md">
            {data?.name}
          </Title>
          <Text mb={"md"}>{data?.introduce}</Text>
          <Title order={2}>Nội Dung Khóa Học</Title>
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 4 }}>
          <Image mb={"md"} src={data?.imageCourse} alt={data.code}/>
          <Group justify="space-between">
            <Title order={3}>{formatCurrencyVND(data?.price)}</Title>
            <Button>Đăng Ký Học</Button>
          </Group>
          <Text>Thời Gian Học : {data?.totalCourseDuration} Ngày</Text>
        </Grid.Col>
      </Grid>
    </RootLayout>
  );
}
