"use client";

import {
  Accordion,
  Alert,
  Button,
  Grid,
  Group,
  Image,
  List,
  ListItem,
  Text,
  Title,
  TypographyStylesProvider,
} from "@mantine/core";
import RootLayout from "../../components/Layout/RootLayout/RootLayout";
import useSWR from "swr";
import {
  BillClient,
  BillCreate,
  CourseClient,
  CourseSubjectClient,
} from "../web-api-client";
import { formatCurrencyVND, handleSubmit } from "../../lib/helper";
import Loading from "../../components/Loading/Loading";
import ActionButton from "../../components/Helper/ActionButton";
import Link from "next/link";

function CourseSubject({ courseId }: { courseId: number }) {
  const { data, isLoading } = useSWR(`/api/courseSubject/${courseId}`, () =>
    new CourseSubjectClient().getEntities(courseId, null, null, null, null)
  );

  return isLoading ? (
    <Loading />
  ) : data ? (
    <Accordion chevronPosition="right" variant="contained">
      {data.items.map((item) => (
        <Accordion.Item
          value={item.subject.id.toString()}
          key={item.subject.id}
        >
          <Accordion.Control>
            <Text>{item.subject.name}</Text>
          </Accordion.Control>
          <Accordion.Panel>
            <List>
              {item.subject.subjectDetails.map((item) => (
                <ListItem key={item.id}>
                  <Text>{item.name}</Text>
                </ListItem>
              ))}
            </List>
          </Accordion.Panel>
        </Accordion.Item>
      ))}
    </Accordion>
  ) : (
    <Alert>Không Có Gì Ở Đây Cả</Alert>
  );
}

export default function Detail({ params }: { params: { id: number } }) {
  const { id } = params;

  const { data, isLoading, mutate } = useSWR(`/api/course/${id}`, () =>
    new CourseClient().getEntity(id)
  );

  return isLoading ? (
    <Loading />
  ) : data ? (
    <RootLayout>
      <Grid py="md">
        <Grid.Col span={{ base: 12, lg: 8 }}>
          <Title order={3} mb="md">
            {data.name}
          </Title>
          <TypographyStylesProvider mb="md">
            <div dangerouslySetInnerHTML={{ __html: data.introduce }} />
          </TypographyStylesProvider>
          <Title order={3} mb="md">
            Nội Dung Khóa Học
          </Title>
          <CourseSubject courseId={id} />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 4 }}>
          <Image mb="md" src={data.imageCourse} alt={data.code} />
          <Group justify="space-between">
            <Title order={3}>{formatCurrencyVND(data.price)}</Title>
            {data.bill ? (
              data.bill.billStatus.name === "Paid" ? (
                <Link href={`/study/${id}`}>
                  <Button>Đi Đến Khóa Học</Button>
                </Link>
              ) : (
                <Button>{data.bill.billStatus.name}</Button>
              )
            ) : (
              <ActionButton
                action={() =>
                  handleSubmit(
                    async () =>
                      new BillClient().createEntity({
                        courseId: id,
                      } as BillCreate),
                    mutate
                  )
                }
              >
                Đăng Ký Học
              </ActionButton>
            )}
          </Group>
          <Text>Thời Gian Học: {data.totalCourseDuration} Ngày</Text>
        </Grid.Col>
      </Grid>
    </RootLayout>
  ) : (
    <Alert>Không Có Gì Ở Đây Cả</Alert>
  );
}
