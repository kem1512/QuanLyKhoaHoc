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
} from "@mantine/core";
import RootLayout from "../../components/Layout/RootLayout/RootLayout";
import useSWR from "swr";
import {
  AccountClient,
  CourseClient,
  RegisterStudyClient,
  RegisterStudyCreate,
} from "../web-api-client";
import { formatCurrencyVND, handleSubmit } from "../../lib/helper";
import Loading from "../../components/Loading/Loading";
import RegisterStudy from "../../components/RegisterStudy/RegisterStudy";
import ActionButton from "../../components/Helper/ActionButton";

export default function Detail({ params }: { params: { id: number } }) {
  const RegisterStudyService = new RegisterStudyClient();

  const { id } = params;

  const { data, isLoading } = useSWR(`/api/course/${id}`, () =>
    new CourseClient().getEntity(id)
  );

  const { data: registerStudy } = useSWR(`/api/registerStudy/${id}`, () =>
    new AccountClient().registerStudy(id)
  );

  return (
    <RootLayout>
      {isLoading ? (
        <Loading />
      ) : data ? (
        <Grid py={"md"}>
          <Grid.Col span={{ base: 12, lg: 8 }}>
            <Title order={3} mb="md">
              {data.name}
            </Title>
            <Text mb={"md"}>{data.introduce}</Text>
            <Title order={3} mb={"md"}>
              Nội Dung Khóa Học
            </Title>
            <Accordion chevronPosition="right" variant="contained">
              {data.courseSubjects.map((item) => {
                if (item.subject?.subjectDetails?.length <= 0) {
                  return;
                }
                return (
                  <Accordion.Item
                    value={item.subject.id.toString()}
                    key={item.subject.name}
                  >
                    <Accordion.Control>
                      <Text>{item.subject.name}</Text>
                    </Accordion.Control>
                    <Accordion.Panel>
                      <List>
                        {item.subject.subjectDetails.map((subjectDetail) => {
                          return (
                            <ListItem key={subjectDetail.id}>
                              <Text>{subjectDetail.name}</Text>
                            </ListItem>
                          );
                        })}
                      </List>
                    </Accordion.Panel>
                  </Accordion.Item>
                );
              })}
            </Accordion>
          </Grid.Col>
          {registerStudy ? (
            <RegisterStudy />
          ) : (
            <Grid.Col span={{ base: 12, lg: 4 }}>
              <Image mb={"md"} src={data.imageCourse} alt={data.code} />
              <Group justify="space-between">
                <Title order={3}>{formatCurrencyVND(data.price)}</Title>
                <ActionButton
                  action={() =>
                    handleSubmit(
                      () =>
                        RegisterStudyService.createEntity({
                          courseId: id,
                        } as RegisterStudyCreate),
                      "Đăng Ký Thành Công"
                    )
                  }
                >
                  Đăng Ký Học
                </ActionButton>
              </Group>
              <Text>Thời Gian Học : {data.totalCourseDuration} Ngày</Text>
            </Grid.Col>
          )}
        </Grid>
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </RootLayout>
  );
}
