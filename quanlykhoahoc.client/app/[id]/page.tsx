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
import RegisterStudy from "../../components/Study/Study";

export default function Detail({ params }: { params: { id: number } }) {
  const { id } = params;

  const { data: courseData, isLoading: isCourseLoading } = useSWR(
    `/api/course/${id}`,
    () => new CourseClient().getEntity(id)
  );

  const {
    data: billData,
    isLoading: isBillLoading,
    mutate,
  } = useSWR(`/api/bill/${id}`, () => new BillClient().getEntity(id));

  const { data: courseSubject, isLoading: isCourseSubjectLoading } = useSWR(
    `/api/courseSubject/${id}`,
    () => new CourseSubjectClient().getEntities(id, null, null, null, null)
  );

  if (isCourseLoading || isBillLoading || isCourseSubjectLoading) {
    return <Loading />;
  }

  const isBillPaid = billData?.billStatus?.name === "Paid";

  return courseData ? (
    isBillPaid ? (
      <RegisterStudy courseId={id} />
    ) : (
      <RootLayout>
        <Grid py="md">
          <Grid.Col span={{ base: 12, lg: 8 }}>
            <Title order={3} mb="md">
              {courseData.name}
            </Title>
            <TypographyStylesProvider mb="md">
              <div dangerouslySetInnerHTML={{ __html: courseData.introduce }} />
            </TypographyStylesProvider>
            <Title order={3} mb="md">
              Nội Dung Khóa Học
            </Title>
            <Accordion chevronPosition="right" variant="contained">
              {courseSubject?.items?.map((item) => (
                <Accordion.Item
                  value={item.subject.id.toString()}
                  key={item.subject.id}
                >
                  <Accordion.Control>
                    <Text>{item.subject.name}</Text>
                  </Accordion.Control>
                  <Accordion.Panel>
                    <List>
                      {item.subject.subjectDetails.map((subjectDetail) => (
                        <ListItem key={subjectDetail.id}>
                          <Text>{subjectDetail.name}</Text>
                        </ListItem>
                      ))}
                    </List>
                  </Accordion.Panel>
                </Accordion.Item>
              ))}
            </Accordion>
          </Grid.Col>
          <Grid.Col span={{ base: 12, lg: 4 }}>
            <Image mb="md" src={courseData.imageCourse} alt={courseData.code} />
            <Group justify="space-between">
              <Title order={3}>{formatCurrencyVND(courseData.price)}</Title>
              {billData ? (
                <Button>{billData.billStatus.name}</Button>
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
            <Text>Thời Gian Học: {courseData.totalCourseDuration} Ngày</Text>
          </Grid.Col>
        </Grid>
      </RootLayout>
    )
  ) : (
    <Alert>Không Có Gì Ở Đây Cả</Alert>
  );
}
