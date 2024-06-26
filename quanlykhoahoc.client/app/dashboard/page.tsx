"use client";

import {
  Alert,
  Center,
  Flex,
  Group,
  Paper,
  RingProgress,
  SimpleGrid,
  Text,
  Title,
  rem,
} from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import { IconArrowUpRight, IconArrowDownRight } from "@tabler/icons-react";
import { BarChart, DonutChart } from "@mantine/charts";
import useSWR from "swr";
import { StatisticalClient } from "../web-api-client";
import { useState } from "react";

const icons = {
  up: IconArrowUpRight,
  down: IconArrowDownRight,
};

const data = [
  {
    label: "Số Khóa Học",
    stats: "456,578",
    progress: 65,
    color: "teal",
    icon: "up",
  },
  {
    label: "Số Học Viên",
    stats: "2,550",
    progress: 72,
    color: "blue",
    icon: "up",
  },
  {
    label: "Doanh Thu Trong Ngày",
    stats: "4,735",
    progress: 52,
    color: "red",
    icon: "down",
  },
];

export default function Dashboard() {
  const [statistical, setStatistical] = useState({
    startDate: new Date(),
    endDate: new Date(),
    topN: 5,
  });

  const { data: check } = useSWR(
    `/api/statistical/${statistical.startDate}/${statistical.endDate}`,
    () =>
      new StatisticalClient().getTotalRevenue(
        statistical.startDate,
        statistical.endDate
      ),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  if (!check)
    return (
      <Flex h={"100vh"} justify={"center"} align={"center"}>
        <Title order={2}>Bạn Không Thể Truy Cập Vào Đây</Title>
      </Flex>
    );
  const stats = data.map((stat) => {
    const Icon = icons[stat.icon];
    return (
      <Paper withBorder radius="md" p="xs" key={stat.label}>
        <Group>
          <RingProgress
            size={80}
            roundCaps
            thickness={8}
            sections={[{ value: stat.progress, color: stat.color }]}
            label={
              <Center>
                <Icon
                  style={{ width: rem(20), height: rem(20) }}
                  stroke={1.5}
                />
              </Center>
            }
          />

          <div>
            <Text c="dimmed" size="xs" tt="uppercase" fw={700}>
              {stat.label}
            </Text>
            <Text fw={700} size="xl">
              {stat.stats}
            </Text>
          </div>
        </Group>
      </Paper>
    );
  });

  return (
    <DashboardLayout>
      <Group gap={"xl"}>
        <SimpleGrid w={"100%"} cols={{ base: 1, sm: 3 }}>
          {stats}
        </SimpleGrid>
        <BarChart
          h={300}
          data={[
            {
              month: "January",
              Smartphones: 1200,
              Laptops: 900,
              Tablets: 200,
            },
            {
              month: "February",
              Smartphones: 1900,
              Laptops: 1200,
              Tablets: 400,
            },
            { month: "March", Smartphones: 400, Laptops: 1000, Tablets: 200 },
            { month: "April", Smartphones: 1000, Laptops: 200, Tablets: 800 },
            { month: "May", Smartphones: 800, Laptops: 1400, Tablets: 1200 },
            { month: "June", Smartphones: 750, Laptops: 600, Tablets: 1000 },
          ]}
          dataKey="month"
          series={[
            { name: "Smartphones", color: "violet.6" },
            { name: "Laptops", color: "blue.6" },
            { name: "Tablets", color: "teal.6" },
          ]}
          tickLine="y"
        />
        <DonutChart
          data={[
            { name: "USA", value: 400, color: "indigo.6" },
            { name: "India", value: 300, color: "yellow.6" },
            { name: "Japan", value: 100, color: "teal.6" },
            { name: "Other", value: 200, color: "gray.6" },
          ]}
        />
      </Group>
    </DashboardLayout>
  );
}
